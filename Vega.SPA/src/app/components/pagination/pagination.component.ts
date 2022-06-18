import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css'],
})
export class PaginationComponent implements OnInit {
  @Input('total-page') totalPage: any = 0;
  @Input('page-size') pageSize: number = 0;
  @Output('paging-change') pagingChange: any = new EventEmitter();
  pages: number[] = [];
  currentPage: number = 1;
  constructor() {}

  ngOnInit() {
    this.pages = [...Array(this.totalPage).keys()].map((i) => i + 1);
  }

  onPageChange(pageNumber: any) {
    if (pageNumber < 1 || pageNumber > this.totalPage) return;
    this.currentPage = pageNumber;
    this.pagingChange.emit({ page: pageNumber, pageSize: this.pageSize });
  }
}
