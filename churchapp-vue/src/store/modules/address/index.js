import axios from 'axios';

export default {
    namespaced: true,
    state: {},
    mutations: {},
    actions: {
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
    },
}