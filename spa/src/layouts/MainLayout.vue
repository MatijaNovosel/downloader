<template>
	<q-layout view="hHh lpR fff">
		<q-header elevated>
			<q-toolbar class="bg-white">
				<q-img class="navbar-img q-ml-md" src="../assets/down-logo.svg"></q-img>
				<span class="text-black text-h6 q-ml-sm">Downloader</span>
				<span class="text-grey q-ml-xs">by Matija</span>
        <q-space />
				<div class="text-black">Quasar v{{ $q.version }}</div>
			</q-toolbar>
		</q-header>
		<q-page-container>
			<router-view />
		</q-page-container>
	</q-layout>
</template>

<script>
import EssentialLink from "components/EssentialLink";

export default {
	name: "MainLayout",
	components: {
		EssentialLink
	},
	data() {
		return {};
	}
};
</script>

<style scoped lang="sass">
.navbar-img
  width: 35px
  height: 35px
</style>
