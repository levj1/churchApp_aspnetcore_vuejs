<template>
  <div>
    <v-row>
      <v-col cols="12">
        <h1>Reports</h1>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="4">
        <datePicker v-bind:dateEntered.sync="fromDate" v-bind:dateLabel.sync="startDateLabel" />
      </v-col>
      <v-col cols="4">
        <datePicker v-bind:dateEntered.sync="toDate" v-bind:dateLabel.sync="endDateLabel" />
      </v-col>
      <v-col cols="4">
        <v-autocomplete
          label="Report Type"
          :items="donationTypes"
          v-model="donationTypeId"
          item-text="type"
          item-value="id"
        ></v-autocomplete>

        <v-btn class="primary" @click="report">Submit</v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-alert dense text type="success" v-if="donations.length > 0">
          Total amount from {{fromDate}} to {{toDate}} is:
          <strong>
            <i class="mdi mdi-currency-usd">{{totalAmount}}</i>
          </strong>.
        </v-alert>
        <v-row>
          <v-col align="right">
            <v-btn class="ma-2" @click="downloadPdf">Pdf</v-btn>
            <v-btn class="ma-2">
              <JsonExcel
                class="ma-2"
                color="primary"
                :data="donations"
                :fields="json_fields"
                type="csv"
                name="donationreport.xls"
              >Excel</JsonExcel>
            </v-btn>
          </v-col>
        </v-row>
        <v-data-table :headers="headers" :items="donations" id="my-table">
          <template v-slot:item.donationDate="{ item }">
            <span>{{formatDate(item.donationDate, 'mm-dd-yyyy')}}</span>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
  </div>
</template>
<script>
import datePicker from "../../shared/CustomDatePicker.vue";
import utilityMixin from "../../shared/mixin/util-mixin.js";
import JsonExcel from "vue-json-excel";
import jsPDF from "jspdf";
import "jspdf-autotable";

export default {
  name: "donationReport",
  mixins: [utilityMixin],
  created() {
    this.$store
      .dispatch("donationType/getDonationTypes")
      .then()
      .catch(err => {
        console.log("An error occured while loading donation types");
      });
    this.checkChange();
  },
  computed: {
    donationTypes() {
      var types = this.$store.state.donationType.donationTypes;
      if (types && types.length > 0) {
        types.unshift({ id: 0, type: "None", description: "" });
      }
      return types;
    }
  },
  components: { datePicker, JsonExcel },
  data: () => ({
    totalAmount: 0,
    donations: [],
    endDateLabel: "End date",
    startDateLabel: "Start date",
    fromDate: new Date().toISOString().substr(0, 10),
    toDate: new Date().toISOString().substr(0, 10),
    donationTypeId: 0,
    headers: [
      { text: "Name", align: "left", value: "person.fullName" },
      { text: "Donation Type", align: "left", value: "donationType.type" },
      { text: "Date", align: "left", value: "donationDate" },
      { text: "Amount", align: "left", value: "amount" }
    ],
    json_fields: {
      Name: "person.fullName",
      "Donation Type": "donationType.type",
      Date: "donationDate",
      Amount: "amount"
    }
  }),
  methods: {
    report() {
      this.$store
        .dispatch("donation/getDonationReport", {
          startDate: this.fromDate,
          endDate: this.toDate,
          donationTypeId: this.donationTypeId
        })
        .then(res => {
          this.donations = res.data;
          this.totalAmount = 0;
          this.donations.forEach(d => {
            this.totalAmount += d.amount;
          });
        })
        .catch(err => {
          console.log("error while getter donations");
        });
    },
    checkChange() {
      let start = new Date(this.fromDate);
      let end = new Date(this.toDate);
      if (end >= start) {
        this.report();
      }
    },
    downloadPdf() {
      var doc = new jsPDF();

      doc.autoTable({
        body: this.donations.map(function(el) {
        return {
          name: el.person.fullName,
          type: el.donationType.type,
          date: el.donationDate,
          amount: el.amount
        };
      }),
        columns: [
          { header: "Name", dataKey: "name" },
          { header: "Donation Type", dataKey: "type" },
          { header: "Donation Date", dataKey: "date" },
          { header: "Amount", dataKey: "amount" }
        ]
      });
      doc.save("donationp.pdf");
    }
  },
  watch: {
    fromDate: function(val) {
      this.checkChange();
    },
    toDate: function(val) {
      this.checkChange();
    },
    donationTypeId: function(val) {
      this.checkChange();
    }
  }
};
</script>