<template>
  <div class="giver">
    <v-card>
      <v-card-title>
        Donations
        <v-spacer></v-spacer>
        <v-btn @click="addDonation">Add Donation</v-btn>
      </v-card-title>
      <v-data-table :headers="headers" :items="donations" :search="search">
        <template v-slot:item.donationDate="{ item }">
          <span>{{formatDate(item.donationDate, 'mm-dd-yyyy')}}</span>
          <!-- <v-icon small @click="deletePerson(item)">mdi-delete</v-icon> -->
        </template>
        <template v-slot:item.action="{ item }">
          <v-icon small class="mr-2" @click="editDonation(item)">mdi-pencil</v-icon>
          <v-icon small @click="deleteDonation(item)">mdi-delete</v-icon>
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
import moment from "moment";

export default {
  name: "giver",
  created() {
    this.$store
      .dispatch("getDonations")
      .then(res => {
        this.donations = res.data;
        this.donations.forEach(a => {
          console.log(this.formatDate(a.donationDate));
        });
      })
      .catch(err => {
        console.log("error while getter donations");
      });
  },
  computed: {
    isLoggedIn() {
      return this.$store.state.currentUser !== null;
    }
  },
  data() {
    return {
      donations: [],
      search: "",
      headers: [
        {
          text: "Full Name",
          align: "left",
          value: "person.fullName"
        },
        {
          text: "Amount",
          align: "left",
          value: "amount"
        },
        {
          text: "Date",
          align: "left",
          value: "donationDate"
        },
        {
          text: "Type",
          align: "left",
          value: "donationType.type"
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
    addDonation() {
      this.$router.push({ name: "AddDonation" });
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
          this.$store.dispatch("getGivers", {
            includeAddress: false,
            includeDonations: false
          });
        });
      }
    },
    formatDate(d, format) {
      const date = new Date(d);
      let month = (date.getMonth() + 1).toString();
      let day = date.getUTCDate().toString();
      const year = date.getFullYear().toString();
      if (month.length < 2) {
        month = "0" + month;
      }
      if (day.length < 2) {
        day = "0" + day;
      }

      switch (format) {
        case "yyyy-mm-dd":
          return [year, month, day].join("-");

        case "mm/dd/yyyy":
          return [month, day, year].join("/");
        
        case "mm-dd-yyyy":
          return [month, day, year].join("-");

        default:
          return [year, month, day].join("-");
      }
    }
  }
};
</script>