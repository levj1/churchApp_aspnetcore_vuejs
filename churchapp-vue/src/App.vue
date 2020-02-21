<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <router-link to="/">
        <v-btn text>
          <span class="mr-4">Home</span>
        </v-btn>
      </router-link>

      <v-spacer></v-spacer>

      <router-link to="/giver">
        <v-btn text>
          <span class="mr-2">Givers</span>
        </v-btn>
      </router-link>
      <div v-if="getUser">
        <span class="mr-2">Hi {{getUser.userName}}</span>
      </div>
    </v-app-bar>

    <v-content>
      <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>
// import HelloWorld from './components/HelloWorld';

export default {
  name: "App",
  computed: {
    getUser(){
      console.log('user: ' + this.$store.state.currentUser);
      return this.$store.state.currentUser;
    }
  },
  components: {
    // HelloWorld,
  },

  data: () => ({
    //
  })
};
</script>
