import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';
import config from '../config';

Vue.use(Vuex)

function removeToken() {
  localStorage.removeItem('token');
}
function setToken(token) {
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
    async getGivers({ commit }) {
      await axios.get(config.BASE_URL + '/api/persons')
        .then(result => commit('updateGivers', result.data))
        .catch()
        ;
    },
    getGiver(context, id) {
      return axios.get(config.BASE_URL + '/api/persons/' + id);
    },
    createGiver(context, person) {
     return axios.post(config.BASE_URL + '/api/persons',
        person,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json'
          }
        })
    },
    deleteGiver(context, id){
      return axios.delete(config.BASE_URL + '/api/persons/' + id),
      {
        headers: {
          'Authorization': 'Bearer ' + this.state.token,
          'Content-Type': 'application/json'
        }
      };
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