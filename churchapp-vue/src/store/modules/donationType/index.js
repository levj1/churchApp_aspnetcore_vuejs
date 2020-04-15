import axios from 'axios';

export default {
    namespaced: true,
    state: {
        donationTypes: [],
    },
    mutations: {
        updateDonationTypes(state, types) {
            state.donationTypes = types;
        },
    },
    actions: {
        async getDonationTypes({ commit }) {
            await axios.get(process.env.VUE_APP_API + '/api/donationTypes')
                .then(result => commit('updateDonationTypes', result.data))
                .catch()
                ;
        },
    },
}