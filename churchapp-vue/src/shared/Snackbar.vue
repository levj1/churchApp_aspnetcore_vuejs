<template>
  <div class="text-center ma-2">
    <v-snackbar :color="colorLocal" v-model="openLocal">
      {{ textLocal }}
      <v-btn text @click="openLocal = false">Close</v-btn>
    </v-snackbar>
  </div>
</template>
<script>
export default {
  props: {
    open: Boolean,
    text: { type: String, default: '' },
    color: String
  },
  mounted() {
    this.openLocal = this.open;
    this.colorLocal = this.color;
    this.textLocal = this.text;
  },
  data: () => ({
    openLocal: false,
    textLocal: "Oops something went wrong",
    colorLocal: "success"
  }),
  watch: {
    openLocal: function(nextValue) {
      // this.$emit('openUpdated', nextValue);
      this.$emit("update:open", nextValue);
    },
    open: function(nextValue) {
      this.openLocal = nextValue;
    },
    textLocal: function(nextValue) {
      let success = true;
      if (success) {
        this.$emit("update:text", nextValue);
      }
    },
    text: function(nextValue) {
      this.textLocal = nextValue;
    },
    colorLocal: function(nextValue) {
      this.$emit("update:color", nextValue);
    },
    color: function(nextValue) {
      this.colorLocal = nextValue;
    }
  }
};
</script>