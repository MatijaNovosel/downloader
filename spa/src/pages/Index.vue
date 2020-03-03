<template>
	<q-page class="flex q-mt-lg q-ml-lg">
		<q-list dense bordered padding class="rounded-borders">
			<template v-for="(artist, i) in artists">
				<q-item :key="artist.name" class="q-my-md">
					<q-item-section avatar>
						<q-img :src="artist.image[0]['#text'] || null" spinner-color="white" style="height: 60px; width: 60px; border: 1px solid #9e9e9e; border-radius: 6px;" />
					</q-item-section>
					<q-item-section top>
						<q-item-label lines="1">
							<span class="text-weight-medium">{{ artist.name }}</span>
						</q-item-label>
						<q-item-label caption lines="1"> <b>MBID: </b> {{ artist.mbid || 'NULL' }}</q-item-label>
					</q-item-section>
					<q-item-section top side>
						<div class="text-grey-8 q-gutter-xs">
							<q-btn class="gt-xs" size="12px" flat dense round icon="done" />
							<q-btn size="12px" flat dense round icon="more_vert" />
						</div>
					</q-item-section>
				</q-item>
				<q-separator :key="i" v-if="i != artists.length - 1 " />
			</template>
		</q-list>
	</q-page>
</template>

<script>
export default {
	name: "PageIndex",
	data() {
		return {
      artists: null
		};
  },
	created() {
		this.$axios
			.get("Test/searchArtist", {
				params: {
					name: "Bijelo"
				}
			})
			.then(({ data }) => {
				this.artists = data.results.artistmatches.artist;
				console.log(this.artists);
			});
	}
};
</script>
