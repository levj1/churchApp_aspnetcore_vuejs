import axios from 'axios';

export default {
    namespaced: true,
    state: {},
    mutations: {},
    actions: {
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
    },
}