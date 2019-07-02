class Heatmap
{
  init() {
    console.log(this.sanitizeHeatmapData());
  }
  
  sanitizeHeatmapData() {
    const dataParsed = JSON.parse(data);

    console.log(this.checkDuplicateInObject(10000, dataParsed));
    //return dataParsed;
  }
  
  // createHeatmap() {
  //   const dataPoints = [dataPoint, dataPoint, dataPoint, dataPoint];
  //   heatmapInstance.addData(dataPoints);
  // }

  checkDuplicateInObject(propertyName, inputArray) {
    let seenDuplicate = false,
      testObject = {};

    inputArray.map(function(item) {
      const itemPropertyName = item[propertyName];
      
      if (itemPropertyName in testObject) {
        testObject[itemPropertyName].duplicate = true;
        item.duplicate = true;
        seenDuplicate = true;
      }
      else {
        testObject[itemPropertyName] = item;
        delete item.duplicate;
      }
    });

    return seenDuplicate;
  }
}

const heatmap = new Heatmap();
heatmap.init();