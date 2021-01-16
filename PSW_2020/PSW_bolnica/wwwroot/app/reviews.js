Vue.component("reviews", {
    data: function () {
        return {
            review: { value: 'proba1', isPublished: 'false' },
            reviews: [{ value: 'proba1', isPublished: 'false' }, { value: 'proba2', isPublished: 'true' }]

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
                            <td>{{review.value}}</td>
                            <td>
                                <button v-if="review.isPublished==='false'" class="buttonChoose" style="background-image: url('../images/publish.png');" v-on:click= "cancel()" type="button"></button>
                                <p v-if="review.isPublished==='true'">already published</p>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    `,
    methods: {

    },
})