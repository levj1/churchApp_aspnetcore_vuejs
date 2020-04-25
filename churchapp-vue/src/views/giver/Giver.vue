<template>
  <div class="giver">
    <div class="text-right pa-2">
      <v-btn color="secondary" @click="addPerson">Add New Person</v-btn>
    </div>

    <v-card>
      <v-card-title>
        Givers
        <v-spacer></v-spacer>
        <v-text-field v-model="search" append-icon="mdi-magnify" label="Search"></v-text-field>
      </v-card-title>
      <v-data-table :headers="headers" :items="givers" :search="search">
        <template v-slot:item.action="{ item }">
          <v-icon class="mr-2" @click="editItem(item)">mdi-pencil</v-icon>
          <v-icon @click="deletePerson(item)">mdi-delete</v-icon>
        </template>

        <template v-slot:item.imageVal="{ }">
          <v-avatar>
            <img src="https://cdn.vuetifyjs.com/images/john.jpg" alt="John" />
          </v-avatar>
        </template>
      </v-data-table>

      <snackBar
        v-if="show"
        v-bind:open.sync="show"
        v-bind:text.sync="message"
        v-bind:color.sync="color"
      ></snackBar>

      <dialogMessage
        v-if="showDialog"
        v-bind:dialog.sync="showDialog"
        v-bind:message.sync="alertMessage"
        v-on:dialogConfirmationEvent="confirmEvent"
      ></dialogMessage>
    </v-card>
  </div>
</template>

<script>
import snackBar from "../../shared/Snackbar";
import dialogMessage from "../../shared/DialogMessage";

export default {
  name: "giver",
  created() {
    this.$store.dispatch("giver/getGivers", {
      includeAddress: false,
      includeDonations: false
    });
  },
  components: { snackBar, dialogMessage },
  computed: {
    givers() {
      return this.$store.state.giver.givers;
    },
    isLoggedIn() {
      return this.$store.state.account.currentUser !== null;
    }
  },
  data() {
    return {
      personToDelete: null,
      showDialog: false,
      alertMessage: "",
      show: false,
      color: "error",
      message: "",
      search: "",
      headers: [
        { text: "image", value: "imageVal", sortable: false },
        {
          text: "Name",
          align: "left",
          value: "fullName"
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
      this.personToDelete = item;

      // Confirm deletion by showing alert message
      this.showDialog = true;
      this.alertMessage =
        "Are you sure you want to delete: " +
        item.firstName +
        " " +
        item.lastName +
        " and all the donations associate with that person?";
    },
    confirmEvent(val) {
      if (val && this.personToDelete) {
        this.$store
          .dispatch("giver/deleteGiver", this.personToDelete.id)
          .then(res => {
            this.message = "You have successfully deleted this person.";
            this.color = "success";
            this.show = true;
            this.$store.dispatch("giver/getGivers", {
              includeAddress: false,
              includeDonations: false
            });
          });
      }
    }
  }
};
</script>