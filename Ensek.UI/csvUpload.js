function upload() {
        fetch("http://localhost:7163/controller/meter-reading-uploads", {
            method: "POST",
            body: ""
        });
        console.log("He");
    }