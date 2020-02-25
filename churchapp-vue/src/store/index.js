import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';

Vue.use(Vuex)

function removeToken(){
  localStorage.removeItem('token');
}
function setToken(token){
  localStorage.setItem('token', token);
}
export default new Vuex.Store({
  state: {
    givers: [],
    currentUser: null,
    loginErrorMessage: '',
    isAuthenticated: false,
    token: localStorage.getItem('token') || '',
  },
  mutations: {
    updateGivers(state, givers) {
      state.givers = givers;
    },
    updateCurrentUser(state, user) {
      state.currentUser = user;
      if (user == null) {
        state.isAuthenticated = true;
      }
    },
    updateLoginError(state, message) {
      state.loginErrorMessage = message;
    },
    logout(state) {
      state.currentUser = null;
      state.isAuthenticated = false;
      removeToken();
      axios.defaults.headers.common['Authorization'] = null
      this.router('/');
    },


  },
  actions: {
    getGivers({ commit }) {
      axios.get('/api/persons')
        .then(result => commit('updateGivers', result.data))
        .catch(console.error);
    },
    registerUser(context, user) {
      axios.post('/api/accounts/register', user)
        .then(result => {
          console.log(result.data);
          console.log('user created successfully!')
        })
        .catch(error => console.log('An error occured while created this user' + error));
    },
    async login({ commit }, user) {
      await axios.post('/api/accounts/login', user)
        .then(result => {
          commit('updateCurrentUser', result.data);
          const token = result.data.bearerToken;
          this.state.token = token;        
          setToken(token);
          // add headers to axiom for future call
          axios.defaults.headers.common['Authorization'] = 'Bearer ' + this.state.token;
        }).catch(err => {
          commit('updateCurrentUser', null);
          commit('updateLoginError', err.message);
          removeToken();
        });
    },
  },
  modules: {
  }
})