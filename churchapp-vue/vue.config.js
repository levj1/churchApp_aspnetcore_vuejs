
module.exports = {
  "transpileDependencies": [
    "vuetify"
  ],
  devServer: {
    proxy: {
      '/api': {
        target: process.env.VUE_APP_API,
        changeOrigin: true,
      }
    }
  }
}