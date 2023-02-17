
window.callnetfromjs = () => {
    DotNet.invokeMethodAsync('Components', 'NameOfTheMethod')
        .then(data => {
            alert(data);
        });
};
