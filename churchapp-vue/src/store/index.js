import Vue from 'vue'
import Vuex from 'vuex'
import accountModule from './modules/account';
import giverModule from './modules/giver';
import donationModule from './modules/donation';
import donationTypeModule from './modules/donationType';
import addressModule from './modules/address';

Vue.use(Vuex)

const store = new Vuex.Store({
  modules: {
    account: accountModule,
    giver: giverModule,
    donation: donationModule,
    donationType: donationTypeModule,
    address: addressModule
  }
})

export default store;