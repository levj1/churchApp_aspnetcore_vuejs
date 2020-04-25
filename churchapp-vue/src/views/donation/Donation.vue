<template>
  <v-app id="inspire">
    <v-row>
      <v-col cols="3">
        <v-navigation-drawer v-model="drawer" fill-height>
          <v-list dense>
            <template v-for="item in items">
              <v-list-item :key="item.text" link :to="{name: item.route}" exact>
                <v-list-item-action>
                  <v-icon>{{ item.icon }}</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>{{ item.text }}</v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </template>
          </v-list>
        </v-navigation-drawer>
      </v-col>
      <v-col>
        <router-view></router-view>
      </v-col>
    </v-row>
  </v-app>
</template>

  <script>
export default {
  props: {
    source: String
  },
  data: () => ({
    drawer: null,
    items: [
      {
        icon: "mdi-format-list-bulleted",
        text: "Donations",
        route: "Donation"
      },
      { icon: "mdi-folder-plus", text: "Add Donation", route: "AddDonation" },
      { icon: "mdi-printer", text: "Reports", route: "DonationReport" }
    ]
  })
};
</script>