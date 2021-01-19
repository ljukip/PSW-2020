Vue.component("perscriptions", {
    data: function () {
        return {
            perscription: '',
            perscriptions: []

        }
    },
    template: `

    <div style="height: 81.7%;">
    <nav style="background-color: lavenderblush;">
        <hr style='background:#c41088;height:4px;'>
        <label class="label1">All perscriptions</label>
        <hr style='background:#c41088;height:4px;'>
    </nav>
    <div id="Div-panel" style="display: inline;">
            <div>
                <table class="myTable">
                    <thead>
                        <tr class="header">
                            <th>perscription</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-bind:key='perscription.value' v-for='perscription in perscriptions'>
                            <td>{{perscription.therapy}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    `,
    methods: {
        getAllPerscriptions() {
            axios
                .get('getPerscriptions')
                .then(Response => (this.perscriptions = Response.data));
        },


    },
    created() {


        if (UserData == {}) {
            this.$router.push('/login');

        }
        this.getAllPerscriptions();
    },
})