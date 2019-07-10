class HeatmapController
{
  init() {
    const domElement = document.getElementById('container');
    var heatmap = h337.create({
      container: domElement
    });

    heatmap.setData({
      data: data
    });
  }
}

const heatmapController = new HeatmapController();
heatmapController.init();