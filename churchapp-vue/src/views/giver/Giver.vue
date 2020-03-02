<template>
  <div class="giver">
    <v-card>
      <v-card-title>Givers</v-card-title>
      <v-data-table :headers="headers" :items="givers" :search="search">
        <template v-slot:item.action="{ item }">
          <v-icon small class="mr-2" @click="editItem(item)">mdi-pencil</v-icon>
          <v-icon small @click="deleteItem(item)">mdi-delete</v-icon>
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
// import store from '../store/index';

export default {
  name: "giver",
  created() {
    this.$store.dispatch("getGivers");
  },
  computed: {
    givers() {
      return this.$store.state.givers;
    },
    isLoggedIn() {
      return this.$store.state.currentUser !== null;
    }
  },
  data() {
    return {
      search: "",
      headers: [
        {
          text: "First Name",
          align: "left",
          value: "firstName"
        },
        {
          text: "Last Name",
          align: "left",
          value: "lastName"
        },
        
        { text: 'Actions', value: 'action', sortable: false },
      ],
      persons: [{ firstName: "", lastName: "" }]
    };
  }
};
</script>