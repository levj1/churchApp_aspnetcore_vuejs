import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    givers: [],
  },
  mutations: {
    updateGivers(state, givers){
      state.givers = givers;
    }
  },
  actions: {
    getGivers( {commit} ){
      axios.get('/api/persons')
      .then(result => commit('updateGivers', result.data))
      .catch(console.error);
    }
  },
  modules: {
  }
})
