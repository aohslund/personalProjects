// boilerplate to create html/css/js files in a new directory, using promises

const fs = require('fs');
const util = require('util')

const makeDir = util.promisify(fs.mkdir);
const makeFile = util.promisify(fs.writeFile);

const htmlData = "<!DOCTYPE html>\n<html>\n<head>\n<title>Index</title>\n</head>\n<body>\n</body>\n</html>";
const cssData = "";
const jsData = "";

makeDir("./app").then(() =>
    makeFile('./app/index.html', htmlData.then(() =>
        makeFile('./app/styles.css', cssData).then(() =>
            makeFile('./app/app.js', jsData).then(() => {
                console.log("Files and folder created!");
            })
        )
    )
)
    .catch(err => {
        console.log(`Error occurs,  
    Error code -> ${err.code}, 
    Error No -> ${err.errno}`);
    })
) 
