<template>
  <q-page class="q-px-md">
    <div class="row">
      <div class="col-12">
        <div class="row">
          <div class="col-12 q-pt-md">
            <q-input dense v-model="searchText" standout label="Artist name" clearable />
          </div>
          <div class="col-12 q-mt-sm q-pl-sm">
            <span class="hint-text"> Press <kbd>ENTER</kbd> when done typing! </span>
          </div>
        </div>
        <div class="row">
          <div v-if="loading" class="col-12 q-pt-md">
            <div class="q-pa-md">
              <q-item>
                <q-item-section avatar>
                  <q-skeleton type="QAvatar" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>
                    <q-skeleton type="text" />
                  </q-item-label>
                  <q-item-label caption>
                    <q-skeleton type="text" width="65%" />
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section avatar>
                  <q-skeleton type="QAvatar" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>
                    <q-skeleton type="text" />
                  </q-item-label>
                  <q-item-label caption>
                    <q-skeleton type="text" width="65%" />
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section avatar>
                  <q-skeleton type="QAvatar" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>
                    <q-skeleton type="text" />
                  </q-item-label>
                  <q-item-label caption>
                    <q-skeleton type="text" width="65%" />
                  </q-item-label>
                </q-item-section>
              </q-item>
            </div>
          </div>
          <div v-else class="col-12 q-pt-md">
            <q-list v-if="artists != null" dense bordered class="rounded-borders">
              <template v-for="(artist, i) in artists">
                <q-item :key="artist.name" class="q-my-md">
                  <q-separator class="q-pr-xs q-mr-sm colored-separator" vertical />
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
                      <q-btn
                        size="12px"
                        flat
                        dense
                        round
                        class="q-mt-sm"
                        icon="remove_red_eye"
                        @click="getArtistInfo(artist.mbid)"
                      />
                    </div>
                  </q-item-section>
                </q-item>
                <q-separator :key="i" v-if="i != artists.length - 1 " />
              </template>
            </q-list>
          </div>
        </div>
      </div>
      <div class="col-12">
        <q-card v-if="selectedArtist" class="my-card q-mx-auto q-my-md">
          <q-img
            :loading="artistImageLoading"
            v-if="selectedArtist.img != null"
            position="center"
            class="artist-img"
            :src="selectedArtist.img"
          />
          <div v-else class="q-py-lg text-center">No image!</div>
          <q-card-section class="q-py-xs bg-primary text-white">
            <div class="row">
              <div class="col text-h6 ellipsis">{{ selectedArtist.name }}</div>
            </div>
          </q-card-section>
          <q-separator />
          <q-card-section class="q-py-sm">
            <div class="text-caption text-grey">{{ selectedArtist.bio.summary }}</div>
          </q-card-section>
          <q-separator />
          <q-card-section class="q-py-xs text-center bg-primary text-white">Albums</q-card-section>
          <q-separator />
          <q-card-section v-if="albumLoading">
            <div>
              <q-item>
                <q-item-section avatar>
                  <q-skeleton type="QAvatar" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>
                    <q-skeleton type="text" />
                  </q-item-label>
                  <q-item-label caption>
                    <q-skeleton type="text" width="65%" />
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section avatar>
                  <q-skeleton type="QAvatar" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>
                    <q-skeleton type="text" />
                  </q-item-label>
                  <q-item-label caption>
                    <q-skeleton type="text" width="65%" />
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section avatar>
                  <q-skeleton type="QAvatar" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>
                    <q-skeleton type="text" />
                  </q-item-label>
                  <q-item-label caption>
                    <q-skeleton type="text" width="65%" />
                  </q-item-label>
                </q-item-section>
              </q-item>
            </div>
          </q-card-section>
          <q-card-section v-else class="q-pt-md">
            <q-list dense bordered v-if="albums.length != 0 && albums != null">
              <template v-for="(album, i) in albums">
                <q-expansion-item
                  group="album"
                  :key="album.name"
                  clickable
                  dense
                  dense-toggle
                  expand-separator
                  @show="getAlbumInfo(album.mbid)"
                  :label="album.name"
                >
                  <q-card v-if="selectedAlbum != null && !albumDetailsLoading">
                    <q-separator />
                    <q-card-section class="text-center">
                      <q-btn
                        @click="startDownload"
                        size="sm"
                        flat
                        round
                        icon="file_download"
                        class="top-right"
                      />
                      <q-img
                        v-if="selectedAlbum.image[3]['#text'] != '' || selectedAlbum.image[3]['#text'] != null"
                        position="top"
                        class="album-img bordered"
                        :src="selectedAlbum.image[3]['#text']"
                      />
                    </q-card-section>
                    <q-separator />
                    <q-card-section class="q-px-none">
                      <q-list dense>
                        <template v-for="(track, i) in selectedAlbum.tracks.track">
                          <q-item :key="track.name">
                            <span class="text-caption">{{ (i + 1) + '. ' + track.name }}</span>
                          </q-item>
                        </template>
                      </q-list>
                    </q-card-section>
                  </q-card>
                  <q-card v-else flat class="my-card q-mx-auto q-my-md">
                    <q-skeleton height="150px" square />
                    <q-card-section>
                      <q-skeleton type="text" class="text-subtitle1" />
                      <q-skeleton type="text" width="50%" class="text-subtitle1" />
                      <q-skeleton type="text" class="text-caption" />
                    </q-card-section>
                  </q-card>
                </q-expansion-item>
                <q-separator :key="i" v-if="i != albums.length - 1 " />
              </template>
            </q-list>
            <div v-else class="text-center">No albums!</div>
          </q-card-section>
        </q-card>
      </div>
    </div>
    <q-dialog persistent v-model="downloadDialog">
      <q-card>
        <q-card-section>
          <div>Please wait while the album finishes downloading...</div>
        </q-card-section>
        <q-separator />
        <q-card-section class="text-center bg-orange-3">
          <q-spinner color="orange" size="5.5em" />
        </q-card-section>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script>
export default {
  name: "Downloader",
  data() {
    return {
      artists: null,
      searchText: null,
      loading: false,
      selectedArtist: null,
      albums: null,
      albumLoading: false,
      selectedAlbum: null,
      albumDetailsLoading: false,
      albumDownloading: false,
      downloadDialog: false
    };
  },
  methods: {
    getArtistInfo(mbid) {
      this.$axios.get(`Test/artist/${mbid}`).then(({ data }) => {
        this.selectedArtist = data.artist;
        this.artistImageLoading = true;
        this.$axios
          .get("Test/artistImage", {
            params: { name: this.selectedArtist.name }
          })
          .then(
            ({ data }) =>
              (this.selectedArtist.img = data.artists[0].strArtistFanart)
          )
          .finally(() => (this.artistImageLoading = false));
        this.albumLoading = true;
        this.$axios
          .get(`Test/artist/album/${mbid}`)
          .then(({ data }) => {
            this.albums = data.topalbums.album.filter(
              x => x.mbid != "" && x.mbid != null
            );
          })
          .finally(() => {
            this.albumLoading = false;
          });
      });
    },
    getAlbumInfo(mbid) {
      this.albumDetailsLoading = true;
      this.$axios
        .get(`Test/album/${mbid}`)
        .then(({ data }) => {
          this.selectedAlbum = data.album;
          this.selectedAlbum.tracks.track.forEach(x => (x.downloading = false));
        })
        .finally(() => {
          this.albumDetailsLoading = false;
        });
    },
    getData() {
      if (!this.searchText) {
        return;
      }
      this.selectedArtist = null;
      this.loading = true;
      this.$axios
        .get("Test/artist", {
          params: {
            name: this.searchText
          }
        })
        .then(({ data }) => {
          this.artists = data.results.artistmatches.artist.filter(
            x => x.mbid != ""
          );
        })
        .finally(() => {
          this.loading = false;
        });
    },
    startDownload() {
      this.downloadDialog = true;
      this.$axios
        .get("Test/download", {
          params: {
            tracks: this.selectedAlbum.tracks.track.map(x => x.name),
            albumName: this.selectedAlbum.name,
            artistName: this.selectedArtist.name
          }
        })
        .then(({ data }) => {
          this.download(
            "application/zip",
            data,
            `${this.selectedAlbum.name}.zip`
          );
        })
        .finally(() => {
          this.downloadDialog = false;
        });
    },
    download(contentType, base64Data, name) {
      let element = document.createElement("a");
      element.setAttribute("href", `data:${contentType};base64, ${base64Data}`);
      element.setAttribute("download", name);
      element.style.display = "none";
      document.body.appendChild(element);
      element.click();
      document.body.removeChild(element);
    }
  },
  created() {
    document.addEventListener("keydown", e => {
      if (e.keyCode === 13) {
        this.getData();
      }
    });
  }
};
</script>

<style scoped lang="scss">
.artist-img {
  width: 100%;
  height: 300px;
}
.album-img {
  width: 30%;
  height: 30%;
  border-radius: 12px;
}
.bordered {
  border: 1px solid #9e9e9e;
}
.top-right {
  position: absolute;
  top: 15px;
  right: 15px;
}
.hint-text {
  font-size: 11px;
  color: rgb(141, 141, 141);
}
</style>
