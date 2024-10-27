import { Component, OnInit } from '@angular/core';
import { MovieDetailDto } from '../models';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from '../services/movie.service';

@Component({
  selector: 'app-movie-detail',
  templateUrl: './movie-detail.component.html',
  styleUrl: './movie-detail.component.scss'
})
export class MovieDetailComponent implements OnInit {
  movie?: MovieDetailDto;

  constructor(
    private route: ActivatedRoute, 
    private _movieService: MovieService
  ) {}

  ngOnInit(): void {
    const movieId = this.route.snapshot.paramMap.get('id');
    if(movieId != null && movieId != undefined){
      this._movieService.getMovieDetails(+movieId)
        .subscribe(data => this.movie = data);
    }
  }

  addToOfflineList(): void {
    if(this.movie && this.movie.id){
    this._movieService.addToOfflineList(this.movie?.id).subscribe(() => {
      alert(`${this.movie?.title} has been added to your offline list.`);
    });
    }
  }

}
