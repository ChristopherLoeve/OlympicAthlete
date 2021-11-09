const baseUrl = "https://school2rest.azurewebsites.net/api/Athletes/"

Vue.createApp({
    data() {
        return {
            athletes: [],
            createName: "",
            createCountry: "",
            createHeight: null,
            removeId: null,
            message: null,
            statusCode: null,
            filterName: null,
            filterCountry: null,
            filterMinHeight: null,
            filterMaxHeight: null,
        }
    },
    methods: {
        async getAll() {
            try{
                const response = await axios.get(baseUrl)
                this.helperPopulateResult(response)
            }
            catch(ex){
                this.helperPopulateError(ex)
            }
        },
        async create() {
            try{
                if (this.createHeight == null) this.createHeight = 0
                const response = await axios.post(baseUrl, { 
                    id: 0, 
                    name: this.createName, 
                    country: this.createCountry, 
                    height: this.createHeight
                })
                this.helperPopulateCreate(response)
            }
            catch(ex){
                this.helperPopulateError(ex)
            }
        },
        async remove() {
            if (this.removeId == null)
            {
                this.statusCode = null
                this.message = "You must enter an ID to delete an athlete"
            }
            else {
                try {
                    const removeUrl = baseUrl + this.removeId
                    console.log(removeUrl)
                    const response = await axios.delete(removeUrl)
                    this.helperPopulateSingle(response)
                }
                catch(ex) {
                    this.helperPopulateError(ex)
                }
            }
        },
        async filterByName(){
            try{
                let filterUrl = ""
                if (this.filterName == null) filterUrl = baseUrl;
                else {
                    filterUrl = baseUrl + "FilterByName/" + this.filterName
                }

                const response = await axios.get(filterUrl)
                this.helperPopulateResult(response)
                this.filterName = null
            }
            catch(ex){
                this.helperPopulateError(ex)
                this.filterName = null
            }
        },
        async filterByCountry(){
            try{
                let filterUrl = ""
                if (this.filterCountry == null) filterUrl = baseUrl;
                else {
                    filterUrl = baseUrl + "FilterByCountry/" + this.filterCountry
                }

                const response = await axios.get(filterUrl)
                this.helperPopulateResult(response)
                this.filterCountry = null
            }
            catch(ex){
                this.helperPopulateError(ex)
                this.filterCountry = null
            }
        }, // http://localhost:34707/api/Athletes/FilterByHeight?minHeight=150&maxHeight=170
        async filterByHeight(){
            try{
                if (this.filterMinHeight == null) this.filterMinHeight = 0
                if (this.filterMaxHeight == null) this.filterMaxHeight = 9999999
                const filterUrl = baseUrl + "FilterByHeight/?minHeight=" + this.filterMinHeight + "&maxHeight=" + this.filterMaxHeight
                const response = await axios.get(filterUrl)
                this.helperPopulateResult(response)
                this.filterMinHeight = null
                this.filterMaxHeight = null
            }
            catch(ex){
                this.helperPopulateError(ex)
                this.filterMinHeight = null
                this.filterMaxHeight = null
            }
        },
        athleteToString(athlete) {
            return "Id: " + athlete.id + ", Name: " + athlete.name + ", Country: " + athlete.country + ", Height: " + athlete.height 
        },
        helperPopulateResult(response)
        {
            this.athletes = response.data
            this.statusCode = response.status
            this.message = response.statusText
        },
        helperPopulateError(ex)
        {
            console.log(ex)
            this.athletes = []
            this.statusCode = null
            this.message = ex.message
        },
        helperPopulateCreate(response)
        {
            this.statusCode = response.status
            this.message = "Athlete " + this.createName +  " created"
            this.createName = null
            this.createCountry = null
            this.createHeight = null
        },
        helperPopulateSingle(response)
        {
            this.statuscode = response.status
            this.athletes = []
            this.athletes.push(response.data)
            this.message = response.statusText
        }
    }
}).mount("#app")