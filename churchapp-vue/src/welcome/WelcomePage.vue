<template>
  <div>
    <v-card class="mx-auto" max-width="500" outlined>
      <v-list-item three-line>
        <v-list-item-content>
          <div class="overline mb-4">Rated the best apps by churches in the world</div>
          <v-list-item-title class="headline mb-1">All Church Apps</v-list-item-title>
          <v-list-item-subtitle>Attendance, Finance, Checkin,...</v-list-item-subtitle>
        </v-list-item-content>

        <v-list-item-avatar tile size="200" color="grey"></v-list-item-avatar>
      </v-list-item>

      <v-card-actions>
        <v-btn text :to="{name: 'Login'}">Login</v-btn>
        <v-btn text>Regester</v-btn>
      </v-card-actions>
    </v-card>
  </div>
</template>