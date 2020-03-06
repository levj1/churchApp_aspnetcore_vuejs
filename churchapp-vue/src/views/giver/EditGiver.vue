<template>
  <v-row justify="center">
    <v-card flat>
      <v-card-title>
        <span class="headline">Person</span>
      </v-card-title>
      <v-card-text>
        <v-container>
          <v-form ref="form" v-model="valid" lazy-validation>
            <v-row>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  v-model="person.firstName"
                  label="First name*"
                  :rules="firstNameRules"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  label="Middle name"
                  v-model="person.middleName"
                  hint="example of helper text only on focus"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  label="Last name*"
                  v-model="person.lastName"
                  :rules="lastNameRules"
                  hint="example of persistent helper text"
                  persistent-hint
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field label="Email*" v-model="person.email"></v-text-field>
              </v-col>

              <!-- <v-row v-if="!person.addresses.length">
                <v-col cols="12" md="12">
                  <h3>Address(es)</h3>
                  <v-divider></v-divider>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field v-model="person.addresses[0].streetLine1"></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field v-model="person.addresses[0].streetLine2"></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field v-model="person.addresses[0].city"></v-text-field>
                </v-col>
              </v-row>-->
              <v-col cols="12" sm="12">
                <v-btn color="primary" @click="addOrEdit()">
                  <span v-if="id > 0">Edit</span>
                  <span v-if="id == 0">Create</span>
                </v-btn>
                <v-btn color="secondary" :to="{name: 'Giver'}">Cancel</v-btn>
              </v-col>
            </v-row>
          </v-form>
        </v-container>
        <small>*indicates required field</small>
      </v-card-text>
    </v-card>
  </v-row>
</template>

<script>
export default {
  name: "GiverTemplate",
  async created() {
    const { id } = this.$route.params;
    if (id > 0) {
      this.id = id;
      await this.$store
        .dispatch("getGiver", id)
        .then(res => {
          if (res && res.data) {
            this.person = res.data;
            console.log(this.person);
          }
        })
        .catch();
    }
  },
  data: () => ({
    valid: true,
    id: 0,
    person: {
      firstName: "",
      middleName: "",
      lastName: "",
      addresses: []
    },
    firstNameRules: [v => !!v || "First Name is required"],
    lastNameRules: [v => !!v || "Middle Name is required"]
  }),
  methods: {
    addOrEdit() {
      if (this.$refs.form.validate()) {
        // Create
        if (this.id == 0) {
          this.$store
            .dispatch("createGiver", JSON.stringify(this.person))
            .then(res => {
              alert("You have succesfully add this person");
              this.$router.push("/giver");
            })
            .catch(err => {
              alert("An error occured");
            });
        }
      }
    }
  }
};
</script>