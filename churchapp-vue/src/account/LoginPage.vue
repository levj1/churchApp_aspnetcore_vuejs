<template>
  <v-card width="400px" class="mx-auto mt-5" v-if="getUser == null">
    <v-list-item v-if="hasErrorLogin">
      <v-list-item-title class="headline text-center">{{errorMessage}}</v-list-item-title>
    </v-list-item>
    <v-card-title class="pb-0">
      <h1>Login</h1>
    </v-card-title>
    <v-card-text>
      <v-form ref="form" v-model="valid" lazy-validation>
        <v-text-field
          v-model="user.username"
          prepend-icon="mdi-account-circle"
          label="User Name"
          required
          :rules="userNameRules"
        ></v-text-field>

        <v-text-field
          v-model="user.password"
          prepend-icon="mdi-eye"
          required
          :rules="passwordRules"
          type="password"
          label="Password"
        ></v-text-field>
      </v-form>
    </v-card-text>
    <v-divider></v-divider>
    <v-card-actions>
      <v-btn color="success" @click="register">Register</v-btn>
      <v-spacer></v-spacer>
      <v-btn color="info" @click="login">Login</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  name: "LoginPage",
  // created(){
  //   this.user = this.$store.state.user;
  // },
  computed: {
    getUser() {
      return this.$store.state.currentUser;
    }
  },
  data: () => ({
    valid: true,
    user: {
      username: "",
      password: ""
    },
    userNameRules: [v => !!v || "Username is required"],
    passwordRules: [v => !!v || "Password is required"],
    hasErrorLogin: false,
    errorMessage: ""
  }),
  methods: {
    register() {
      this.$router.push("/register");
    },
    async login() {
      if (this.$refs.form.validate()) {
        await this.$store
          .dispatch("login", this.user)
          .then( this.$router.push("/"))
          .catch(e => {
            console.log(e);
            this.hasErrorLogin = true;
            this.errorMessage = "Invalid user/password, please try again.";
          })
          ;
      }
    }
  }
};
</script>
