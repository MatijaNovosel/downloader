<template>
	<q-page class="flex q-mt-lg q-ml-lg">
		<div class="row">
			<div class="col-12 q-pt-md">
        <div class="row">
          <div class="col-9">
            <q-input v-model="searchText" outlined label="Artist name" />
          </div>
          <div class="col-3">
            <q-btn flat color="primary" @click="getData"> Search </q-btn>
          </div>
        </div>
			</div>
		</div>
    <div class="row">
			<div class="col-12 q-pt-md">
				<q-list dense bordered padding class="rounded-borders">
					<template v-for="(artist, i) in artists">
						<q-item :key="artist.name" class="q-my-md">
							<q-item-section avatar>
								<q-img
									:src="artist.image[0]['#text'] || null"
									spinner-color="white"
									style="height: 60px; width: 60px; border: 1px solid #9e9e9e; border-radius: 6px;"
								/>
							</q-item-section>
							<q-item-section top>
								<q-item-label lines="1">
									<span class="text-weight-medium">{{ artist.name }}</span>
								</q-item-label>
								<q-item-label caption lines="1">
									<b>MBID:</b>
									{{ artist.mbid || 'NULL' }}
								</q-item-label>
							</q-item-section>
							<q-item-section top side>
								<div class="text-grey-8 q-gutter-xs">
									<q-btn size="12px" flat dense round icon="remove_red_eye" />
								</div>
							</q-item-section>
						</q-item>
						<q-separator :key="i" v-if="i != artists.length - 1 " />
					</template>
				</q-list>
			</div>
		</div>
	</q-page>
</template>

<script>
export default {
	name: "PageIndex",
	data() {
		return {
			artists: null,
			searchText: null
		};
	},
	methods: {
		getData() {
			if (this.searchText == "" || this.searchText == null) {
        return;
      }
      this.$axios
        .get("Test/searchArtist", {
          params: {
            name: this.searchText
          }
        })
        .then(({ data }) => {
          this.artists = data.results.artistmatches.artist;
        });
		}
	},
	created() {
		this.getData();
	}
};
</script>
