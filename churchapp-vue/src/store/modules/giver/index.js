import axios from 'axios';

export default {
    namespaced: true,
    state: {
        givers: [],
    },
    mutations: {
        updateGivers(state, givers) {
            state.givers = givers;
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
    },
}