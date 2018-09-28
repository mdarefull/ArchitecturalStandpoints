import { BoilerPlateTemplatePage } from './app.po';

describe('BoilerPlate App', function() {
  let page: BoilerPlateTemplatePage;

  beforeEach(() => {
    page = new BoilerPlateTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
