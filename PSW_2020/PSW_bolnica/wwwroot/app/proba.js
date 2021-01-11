Vue.component("proba", {
    data: function () {
        axios
            .get('/methodx')
            .then((responce) => console.log(responce.data))

        return {}

    },
    template: `

    `,
    methods: {

    }


})