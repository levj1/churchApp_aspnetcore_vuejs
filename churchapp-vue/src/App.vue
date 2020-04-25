<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <router-link to="/">
        <v-btn text>
          <span class="mr-4">Home</span>
        </v-btn>
      </router-link>

      <v-spacer></v-spacer>

      <router-link to="/login" v-if="getUser == null"><v-btn text>Login</v-btn></router-link>
      <router-link to="/giver" v-if="getUser">
        <v-btn text>Givers</v-btn>
      </router-link>
      <router-link to="/donation" v-if="getUser">
        <v-btn text>Donations</v-btn>
      </router-link>
      <div v-if="getUser">
        <span class="mr-2">Hi {{getUser.userName}}</span>
        <v-btn text @click="logout">(Logout)</v-btn>
      </div>
    </v-app-bar>

    <v-content>
      <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>

export default {
  name: "App",
  created(){
  },
  computed: {
    getUser(){
      return this.$store.state.account.currentUser;
    }
  },
  components: {
    // HelloWorld,
  },

  data: () => ({
    //
  }),
  methods: {
    logout(){
      this.$store.commit('logout');
      this.$router.push({ name : 'Home'});
    }
  }
};
</script>
