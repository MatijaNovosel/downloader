<template>
  <q-page class="q-pa-md">
    <div class="row">
      <div class="col-6">
        <div class="row">
          <div class="col-12 text-right q-pr-md">
            <span class="text-weight-light text-h6">Settings</span>
          </div>
          <div class="col-12 q-py-sm">
            <q-separator />
          </div>
          <div class="col-12 q-py-none text-center justify-end">
            <div class="row justify-end">
              <q-list dense>
                <q-item active-class="aktivno" clickable v-ripple :active="$route.name == 'settings-account'">
                  <q-item-section class="non-selectable" @click="$router.push({ name: 'settings-account' })">My account</q-item-section>
                  <q-item-section side>
                    <q-icon name="mdi-account" size="xs" />
                  </q-item-section>
                </q-item>
                <q-item clickable v-ripple :active="$route.name == 'settings-appearance'">
                  <q-item-section class="non-selectable" @click="$router.push({ name: 'settings-appearance' })">Appearance</q-item-section>
                  <q-item-section side>
                    <q-icon name="mdi-image" size="xs" />
                  </q-item-section>
                </q-item>
              </q-list>
            </div>
          </div>
        </div>
      </div>
      <div class="col-6">
        <router-view />
      </div>
    </div>
    <q-btn class="bg-green dolje-desno"> Save </q-btn>
  </q-page>
</template>

<script>
export default {
  name: "Settings"
};
</script>

<style scoped lang="sass">
.dolje-desno
  position: fixed
  bottom: 15px
  right: 15px
</style>
