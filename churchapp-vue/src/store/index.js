import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    givers: [],
    currentUser: null,
  },
  mutations: {
    updateGivers(state, givers) {
      state.givers = givers;
    },
    updateCurrentUser(state, user) {
      state.currentUser = user;
    }

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
    login({ commit }, user) {
      axios.post('/api/accounts/login', user)
        .then(result => {
          console.log(result.data);
          commit('updateCurrentUser', result.data);
          console.log('login successful');
        }).catch(err => {
          commit('updateCurrentUser', null);
          console.log(err);
        });
    },
  },
  modules: {
  }
})
