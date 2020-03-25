<template>
  <div class="text-center ma-2">
    <v-snackbar v-model="showSnackbar"
    :color="barColor">
      {{ message }}
      <v-btn dark text @click="showSnackbar = false">Close</v-btn>
    </v-snackbar>
  </div>
</template>
<script>
export default {
  props: {
    message: String,
    showSnackbar: Boolean,    
    barColor: String,
  },
  data: () => ({      
  })
};
</script>