import { Component, OnInit } from '@angular/core';
import { MovieDto } from '../models';
import { MovieService } from '../services/movie.service';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrl: './movie-list.component.scss'
})
export class MovieListComponent implements OnInit {
  movies: MovieDto[] = [];

  constructor(private _movieService: MovieService) {}

  ngOnInit(): void {
    this.fetchPopularMovies();
  }

  fetchPopularMovies(): void {
    this._movieService.getPopularMovies().subscribe((data) => {
      this.movies = data;
    });
  }
}
