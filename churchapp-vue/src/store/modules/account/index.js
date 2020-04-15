import axios from 'axios';

function removeToken() {
    localStorage.removeItem('token');
}
function setToken(token) {
    localStorage.setItem('token', token);
}
export default {

    namespaced: true,
    state: {
        currentUser: null,
        isAuthenticated: false,
        token: localStorage.getItem('token') || '',
    },
    mutations: {
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
        registerUser(context, user) {
            axios.post(process.env.VUE_APP_API + '/api/accounts/register', user)
                .then()
                .catch();
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
}