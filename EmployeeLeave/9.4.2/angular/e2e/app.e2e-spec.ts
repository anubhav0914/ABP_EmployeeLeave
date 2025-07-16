import { EmployeeLeaveTemplatePage } from './app.po';

describe('EmployeeLeave App', function() {
  let page: EmployeeLeaveTemplatePage;

  beforeEach(() => {
    page = new EmployeeLeaveTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
