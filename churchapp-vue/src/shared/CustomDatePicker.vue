<template>
  <v-container>
    <v-row>
      <v-col cols="6">
        <v-menu
          ref="menu1"
          v-model="menu1"
          :close-on-content-click="false"
          transition="scale-transition"
          offset-y
          max-width="290px"
          min-width="290px"
        >
          <template v-slot:activator="{ on }">
            <v-text-field
              v-model="dateFormatted"
              :label="dateLabelLocal"
              persistent-hint
              prepend-icon="event"
              @blur="date = parseDate(dateFormatted)"
              v-on="on"
            ></v-text-field>
          </template>
          <v-date-picker v-model="date" no-title @input="menu1 = false"></v-date-picker>
        </v-menu>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
export default {
  props: {
    dateLabel: {
      type: String,
      default: "Date"
    },
    dateEntered: {
      type: String,
      default: new Date().toISOString().substr(0, 10)
    }
  },
  data: vm => ({
    date: new Date().toISOString().substr(0, 10),
    dateFormatted: vm.formatDate(new Date().toISOString().substr(0, 10)),
    menu1: false,
    dateLabelLocal: "Date"
  }),
  computed: {
    computedDateFormatted() {
      return this.formatDate(this.date);
    }
  },
  mounted() {
    this.dateLabelLocal = this.dateLabel;
  },

  watch: {
    date(val) {
      this.dateFormatted = this.formatDate(this.date);

      this.$emit("update:dateEntered", this.dateFormatted);
    },
    dateLabelLocal: function(nextVal) {
      this.$emit("update:dateLabel", nextVal);
    },
    dateLabel: function(nextVal) {
      this.dateLabelLocal = nextVal;
    }
  },

  methods: {
    formatDate(date) {
      if (!date) return null;

      const [year, month, day] = date.split("-");
      return `${month}/${day}/${year}`;
    },
    parseDate(date) {
      if (!date) return null;

      const [month, day, year] = date.split("/");
      return `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;
    }
  }
};
</script>