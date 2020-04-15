<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-bar dense class="q-electron-drag bg-black">
        <q-icon name="mdi-flask-empty" />
        <div>Application</div>
        <q-space />
        <q-btn dense flat icon="minimize" @click="minimize" />
        <q-btn dense flat :icon="maximized ? 'mdi-dock-window' : 'crop_square'" @click="maximize" />
        <q-btn dense flat icon="close" @click="closeApp" />
      </q-bar>
      <div class="text-caption q-pa-sm q-pl-md row items-center bg-dark">
        <q-btn flat round dense icon="menu" size="sm" />
        <div @click="$router.push({ name: 'home' });" class="q-ml-md cursor-pointer non-selectable">Home</div>
        <div @click="$router.push({ name: 'downloader' });" class="q-ml-md cursor-pointer non-selectable">Downloader</div>
        <q-space />
        <q-btn
          @click="$router.push({ name: 'settings-account' })"
          flat
          round
          dense
          icon="mdi-cog"
          class="q-mr-md"
          size="sm"
        />
      </div>
    </q-header>
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
export default {
  name: "MainLayout",
  methods: {
    minimize() {
      if (process.env.MODE === "electron") {
        this.$q.electron.remote.BrowserWindow.getFocusedWindow().minimize();
        this.maximized = false;
      }
    },
    maximize() {
      if (process.env.MODE === "electron") {
        const win = this.$q.electron.remote.BrowserWindow.getFocusedWindow();
        if (win.isMaximized()) {
          win.unmaximize();
          this.maximized = false;
        } else {
          win.maximize();
          this.maximized = true;
        }
      }
    },
    closeApp() {
      if (process.env.MODE === "electron") {
        this.$q.electron.remote.BrowserWindow.getFocusedWindow().close();
      }
    }
  },

  data() {
    return {
      maximized: false
    };
  }
};
</script>
