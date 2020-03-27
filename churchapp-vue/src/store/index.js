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
    donationTypes: [],
  },
  mutations: {
    updateGivers(state, givers) {
      state.givers = givers;
    },
    updateDonationTypes(state, types) {
      state.donationTypes = types;
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
    async getGivers({ commit }, payload) {
      await axios.get(config.BASE_URL + '/api/persons?includeAddress='
        + payload.includeAddress + '&includeDonations=' + payload.includeDonations)
        .then(result => commit('updateGivers', result.data))
        .catch()
        ;
    },
    getGiver(context, payload) {
      return axios.get(config.BASE_URL + '/api/persons/' + payload.id
        + '?includeAddress=' + payload.includeAddress + '&includeDonations=' + payload.includeDonations);
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
    editGiver(context, person) {
      return axios.put(config.BASE_URL + '/api/persons',
        person,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json'
          }
        })
    },
    deleteGiver(context, id) {
      return axios.delete(config.BASE_URL + '/api/persons/' + id),
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json'
          }
        };
    },
    getDonations(){
      return axios.get(config.BASE_URL + '/api/donations',
      {
        headers: {
          'Authorization': 'Bearer ' + this.state.token,
          'Content-Type': 'application/json'
        }
      });
    },
    createDonations(context, donations){
      return axios.post(config.BASE_URL + '/api/donations/createDonations', donations,
      {
        headers: {
          'Authorization': 'Bearer ' + this.state.token,
          'Content-Type': 'application/json'
        }
      })
    },
    async getDonationTypes({commit}) {
      await axios.get(config.BASE_URL + '/api/donationTypes')
        .then(result => commit('updateDonationTypes', result.data))
        .catch()
        ;
    },
    registerUser(context, user) {
      axios.post('/api/accounts/register', user)
        .then()
        .catch();
    },
    async editAddressForPerson(context, payload) {

      return await axios.put(config.BASE_URL + '/api/persons/' + payload.personId + '/addresses',
        payload.address,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json; charset=utf-8'
          }
        });
    },
    addAddressForPerson(context, payload) {
      return axios.post(config.BASE_URL + '/api/persons/' + payload.personId + '/addresses',
        payload.address,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json; charset=utf-8'
          }
        });
    },
    deleteAddress(context, payload) {
      return axios.delete(config.BASE_URL + '/api/persons/'
        + payload.personId + '/addresses/' + payload.addressId,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json;'
          }
        })
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