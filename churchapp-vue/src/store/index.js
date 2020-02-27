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

const store = new Vuex.Store({
  state: {
    givers: [],
    currentUser: null,
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
        state.isAuthenticated = false;
        removeToken();
      }
    },
    logout(state) {
      state.currentUser = null;
      state.isAuthenticated = false;
      removeToken();
      axios.defaults.headers.common['Authorization'] = null
    },


  },
  actions: {
    getGivers({ commit }) {
      axios.get('/api/persons')
        .then(result => commit('updateGivers', result.data))
        .catch()
        ;
    },
    registerUser(context, user) {
      axios.post('/api/accounts/register', user)
        .then()
        .catch();
    },
    /*eslint no-unused-vars: "error"*/
    async login({ commit }, user) {
      await axios.post('/api/accounts/login', user)
        .then(result => {
          commit('updateCurrentUser', result.data);
          const token = result.data.bearerToken;
          this.state.token = token;        
          this.state.isAuthenticated = true;        
          setToken(token);
          // add headers to axiom for future call
          axios.defaults.headers.common['Authorization'] = 'Bearer ' + this.state.token;
        }).catch( 
          commit('updateCurrentUser', null) 
        );
    },
  },
  modules: {
  }
})

export default store;