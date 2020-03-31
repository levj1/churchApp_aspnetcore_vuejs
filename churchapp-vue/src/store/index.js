import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';

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
      await axios.get(process.env.VUE_APP_API + '/api/persons?includeAddress='
        + payload.includeAddress + '&includeDonations=' + payload.includeDonations)
        .then(result => commit('updateGivers', result.data))
        .catch()
        ;
    },
    getGiver(context, payload) {
      return axios.get(process.env.VUE_APP_API + '/api/persons/' + payload.id
        + '?includeAddress=' + payload.includeAddress + '&includeDonations=' + payload.includeDonations);
    },
    createGiver(context, person) {
      return axios.post(process.env.VUE_APP_API + '/api/persons',
        person,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json'
          }
        })
    },
    editGiver(context, person) {
      return axios.put(process.env.VUE_APP_API + '/api/persons',
        person,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json'
          }
        })
    },
    deleteGiver(context, id) {
      return axios.delete(process.env.VUE_APP_API + '/api/persons/' + id),
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json'
          }
        };
    },
    getDonations() {
      return axios.get(process.env.VUE_APP_API + '/api/donations',
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json'
          }
        });
    },
    getADonation(context, id) {
      return axios.get(process.env.VUE_APP_API + '/api/donations/' + id,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json'
          }
        });
    },
    createDonations(context, donations) {
      return axios.post(process.env.VUE_APP_API + '/api/donations/createDonations', donations,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json'
          }
        })
    },
    deleteDonation(context, id) {
      return axios.delete(process.env.VUE_APP_API + '/api/donations/' + id,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-type': 'application/json'
          }
        });
    },
    async getDonationTypes({ commit }) {
      await axios.get(process.env.VUE_APP_API + '/api/donationTypes')
        .then(result => commit('updateDonationTypes', result.data))
        .catch()
        ;
    },
    registerUser(context, user) {
      axios.post(process.env.VUE_APP_API + '/api/accounts/register', user)
        .then()
        .catch();
    },
    async editAddressForPerson(context, payload) {

      return await axios.put(process.env.VUE_APP_API + '/api/persons/' + payload.personId + '/addresses',
        payload.address,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json; charset=utf-8'
          }
        });
    },
    addAddressForPerson(context, payload) {
      return axios.post(process.env.VUE_APP_API + '/api/persons/' + payload.personId + '/addresses',
        payload.address,
        {
          headers: {
            'Authorization': 'Bearer ' + this.state.token,
            'Content-Type': 'application/json; charset=utf-8'
          }
        });
    },
    deleteAddress(context, payload) {
      return axios.delete(process.env.VUE_APP_API + '/api/persons/'
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
      await axios.post(process.env.VUE_APP_API + '/api/accounts/login', user)
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