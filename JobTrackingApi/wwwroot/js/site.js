const uri = 'http://localhost:55414/api/jobs';
let jobs = [];

function getJobs() {
    
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get jobs.', error));
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'Job' : 'Jobs';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('jobs');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let JobId = document.createTextNode(item.jobId);
        td1.appendChild(JobId);

        let td2 = tr.insertCell(1);
        let CreatedDate = document.createTextNode(item.createdDate);
        td2.appendChild(CreatedDate);

        let td3 = tr.insertCell(2);
        let Duration = document.createTextNode(item.duration);
        td3.appendChild(Duration);

        let td4 = tr.insertCell(3);
        let Status = document.createTextNode(item.status);
        td4.appendChild(Status);       
    });

    jobs = data;
}