<template>
	<q-page class="flex flex-center q-mt-lg">
		<q-list dense bordered padding class="rounded-borders">
			<template v-for="(artist, i) in artists">
				<q-item :key="artist.name" class="q-my-md">
					<q-item-section avatar>
						<q-icon name="account_tree" color="black" size="34px" />
					</q-item-section>
					<q-item-section top>
						<q-item-label lines="1">
							<span class="text-weight-medium"> {{ artist.name }} </span>
							<span class="text-grey-8"> - GitHub repository</span>
						</q-item-label>
						<q-item-label caption lines="1">@rstoenescu in #1: > The build system</q-item-label>
						<q-item-label
							lines="1"
							class="q-mt-xs text-body2 text-weight-bold text-primary text-uppercase"
						>
							<span class="cursor-pointer">Open in GitHub</span>
						</q-item-label>
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
