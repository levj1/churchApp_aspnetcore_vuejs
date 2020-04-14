<template>
  <v-row justify="center">
    <v-dialog v-model="dialogLocal" max-width="400">
      <v-card>
        <v-card-title class="headline text-xs-center">Confirm</v-card-title>
        <v-card-text style="color:black">{{ message }}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="secondary" @click="isConfirm(false)">No</v-btn>
          <v-btn color="primary" @click="isConfirm(true)">Yes</v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<script>
export default {
    name: 'dialogMessage',
    props: {
        dialog: Boolean,
        message: {
            type: String,
            default: 'This is a long message'
        },
        confirm: Boolean
    },
    mounted(){
        this.dialogLocal = this.dialog;
        this.messageLocal = this.message;
        this.confirmLocal = this.confirm;
    },
    data: () => ({
        dialogLocal: false,
        messageLocal: 'Please confirm',
        confirmLocal: false,
    }),
    methods: {
        isConfirm(value = false){
            this.dialogLocal = false;
            this.confirmLocal = value;
            this.$emit('dialogConfirmationEvent', value, 1);
        }
    },
    watch: {
        dialogLocal: function(nextVal){
            this.$emit('update:dialog', nextVal);
        },
        dialog: function(nextVal){
            this.dialogLocal = nextVal;
        },
        messageLocal: function(nextVal){
            this.$emit('update:message', nextVal);
        },
        message: function(nextVal){
            this.dialogLocal = nextVal;
        }
    }
}
</script>