<template>
  <div class="giver">
    <v-card>
      <v-card-title>
        Givers
        <v-spacer></v-spacer>
        <v-btn @click="addPerson">Add New Person</v-btn>
      </v-card-title>
      <v-data-table :headers="headers" :items="givers" :search="search">
        <template v-slot:item.action="{ item }">
          <v-icon small class="mr-2" @click="editItem(item)">mdi-pencil</v-icon>
          <v-icon small @click="deletePerson(item)">mdi-delete</v-icon>
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
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

        { text: "Actions", value: "action", sortable: false }
      ],
      persons: [{ firstName: "", lastName: "", middleName: "", addresses: [] }]
    };
  },
  methods: {
    editItem(item) {
      this.$router.push({ name: "EditGiver", params: { id: item.id } });
    },
    addPerson() {
      this.$router.push({ name: "EditGiver", params: { id: 0 } });
    },
    deletePerson(item) {
      let resp = confirm(
        "Are you sure you want to delete: " +
          item.firstName +
          " " +
          item.lastName +
          "?"
      );
      if (resp) {
        this.$store.dispatch("deleteGiver", item.id).then(res => {
          alert("item deleted");
          this.$store.dispatch("getGivers");
        });
      }
    }
  }
};
</script>