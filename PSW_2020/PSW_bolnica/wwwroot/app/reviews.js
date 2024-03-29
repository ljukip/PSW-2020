Vue.component("reviews", {
    data: function () {
        return {
            review: { value: 'proba1', isPublished: 'false' },
            reviews: []

        }
    },
    template: `

    <div style="height: 81.7%;">
    <nav style="background-color: lavenderblush;">
        <hr style='background:#c41088;height:4px;'>
        <label class="label1">All reviews</label>
        <hr style='background:#c41088;height:4px;'>
    </nav>
    <div id="Div-panel" style="display: inline;">
            <div>
                <table class="myTable">
                    <thead>
                        <tr class="header">
                            <th>review</th>
                            <th>publish</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-bind:key='review.value' v-for='review in reviews'>
                            <td>{{review.text}}</td>
                            <td>
                                <button v-if="!review.isPublished" class="buttonChoose" style="background-image: url('../images/publish.png');" v-on:click= "publish(review.id)" type="button"></button>
                                <p v-if="review.isPublished" class="buttonChoose" style="background-image: url('../images/cnc.png');" v-on:click= "unpublish(review.id)" type="button"></p>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    `,
    methods: {
        publish(id) {
            Swal.fire({
                title: 'Are you sure you want to publish the feedback ?',
                text: "Feedback will be published",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#ffdff0',
                cancelButtonColor: '#c41088',
                confirmButtonText: 'Yes, Im sure!'
            }).then((result) => {
                if (result.isConfirmed) {
                    //block user
                    axios
                        .put(`publish/` + id)
                        .then(Response => {

                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Feedback has been published!',
                                showConfirmButton: false,
                                timer: 3500
                            })
                            setTimeout(() => window.location.reload(), 3500);
                        })

                }
            })
        },
        unpublish(id) {
            Swal.fire({
                title: 'Are you sure you want to take down the feedback ?',
                text: "Feedback will be taken down",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#ffdff0',
                cancelButtonColor: '#c41088',
                confirmButtonText: 'Yes, Im sure!'
            }).then((result) => {
                if (result.isConfirmed) {
                    axios
                        .put(`unpublish/` + id)
                        .then(Response => {

                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Feedback has been taken down!',
                                showConfirmButton: false,
                                timer: 3500
                            })
                            setTimeout(() => window.location.reload(), 3500);
                        })

                }
            })
        },
        getAllReviews() {
            axios
                .get('getFeedbacks')
                .then(Response => (this.reviews = Response.data));
        },


    },
    created() {


        if (UserData == {}) {
            this.$router.push('/login');

        }
        this.getAllReviews();
    },
})