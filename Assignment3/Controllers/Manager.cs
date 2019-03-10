using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment3.EntityModels;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Employee, EmployeeBaseViewModel>();
                cfg.CreateMap<EmployeeAddViewModel, Employee>();
                cfg.CreateMap<EmployeeBaseViewModel, EmployeeEditFormViewModel>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<TrackAddViewModel, TrackAddFormViewModel>();
                cfg.CreateMap<Invoice, InvoiceBaseViewModel>();
                cfg.CreateMap<Invoice, InvoiceWithDetailViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineBaseViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineWithDetailViewModel>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();
                cfg.CreateMap<Playlist, PlaylistBaseViewModel>();
                cfg.CreateMap<Playlist, PlaylistWithDetailViewModel>();
                cfg.CreateMap<PlaylistWithDetailViewModel, PlaylistEditTracksFormViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        public IEnumerable<EmployeeBaseViewModel> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBaseViewModel>>(ds.Employees);
        }

        public EmployeeBaseViewModel EmployeeGetById(int id)
        {
            // Attempt to fetch the object.
            var obj = ds.Employees.Find(id);

            // Return the result (or null if not found).
            return obj == null ? null : mapper.Map<Employee, EmployeeBaseViewModel>(obj);
        }

        public EmployeeBaseViewModel EmployeeAdd(EmployeeAddViewModel newEmployee)
        {
            // Attempt to add the new item.
            // Notice how we map the incoming data to the Employee design model class.
            var addedItem = ds.Employees.Add(mapper.Map<EmployeeAddViewModel, Employee>(newEmployee));
            ds.SaveChanges();

            // If successful, return the added item (mapped to a view model class).
            return addedItem == null ? null : mapper.Map<Employee, EmployeeBaseViewModel>(addedItem);
        }

        public EmployeeBaseViewModel EmployeeEdit(EmployeeEditViewModel newItem)
        {
            var o = ds.Employees.Find(newItem.EmployeeId);

            if (o == null)
            {
                return null;
            }
            else
            {
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                return mapper.Map<EmployeeBaseViewModel>(o);
            }
        }

        public bool EmployeeDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Employees.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Employees.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            var o = ds.Tracks.OrderBy(t => t.Name).ThenBy(t => t.TrackId);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(o);
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetAllWithDetail()
        {
            var o = ds.Tracks.Include("Album.Artist").Include("MediaType").OrderBy(t => t.TrackId);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(o);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllPop()
        {
            var o = ds.Tracks.Where(t => t.GenreId == 9).OrderBy(t => t.Name);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(o);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllDeepPurple()
        {
            var o = ds.Tracks.Where(t => t.Composer.Contains("Jon Lord")).OrderBy(t => t.TrackId);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(o);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllTop100Longest()
        {
            var o = ds.Tracks.OrderByDescending(t => t.Milliseconds).Take(100);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(o);
        }

        public TrackWithDetailViewModel TrackGetByIdWithDetail(int id)
        {
            var obj = ds.Tracks
                        .Include("Album.Artist")
                        .Include("MediaType")
                        .SingleOrDefault(t => t.TrackId == id);
            return obj == null ? null : mapper.Map<Track, TrackWithDetailViewModel>(obj);
        }

        public TrackWithDetailViewModel TrackAdd(TrackAddViewModel newTrack)
        {
            var a = ds.Albums.Find(newTrack.AlbumId);
            var b = ds.MediaTypes.Find(newTrack.MediaTypeId);

            if (a == null || b == null)
            {
                return null;
            }
            else
            {
                var addedItem = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));
                addedItem.Album = a;
                addedItem.MediaType = b;
                ds.SaveChanges();
                return addedItem == null ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedItem);
            }
        }

        public IEnumerable<InvoiceBaseViewModel> InvoiceGetAll()
        {
            var o = ds.Invoices.OrderBy(i => i.InvoiceId);
            return mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBaseViewModel>>(o);
        }

        public InvoiceBaseViewModel InvoiceGetById(int id)
        {
            // Attempt to fetch the object.
            var obj = ds.Invoices.Find(id);

            // Return the result (or null if not found).
            return obj == null ? null : mapper.Map<Invoice, InvoiceBaseViewModel>(obj);
        }

        public InvoiceWithDetailViewModel InvoiceGetByIdWithDetail(int id)
        {
            var obj = ds.Invoices
                        .Include("Customer.Employee")
                        .Include("InvoiceLines.Track.Album.Artist")
                        .Include("InvoiceLines.Track.MediaType")
                        .SingleOrDefault(i => i.InvoiceId == id);
            return obj == null ? null : mapper.Map<Invoice, InvoiceWithDetailViewModel>(obj);
        }

        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var o = ds.Albums.OrderBy(a => a.AlbumId);
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(o);
        }

        public AlbumBaseViewModel AlbumGetById(int id)
        {
            var obj = ds.Albums.Find(id);
            return obj == null ? null : mapper.Map<Album, AlbumBaseViewModel>(obj);
        }

        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var o = ds.Artists.OrderBy(a => a.ArtistId);
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(o);
        }

        public ArtistBaseViewModel ArtistGetById(int id)
        {
            var obj = ds.Artists.Find(id);
            return obj == null ? null : mapper.Map<Artist,ArtistBaseViewModel>(obj);
        }

        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll()
        {
            var o = ds.MediaTypes.OrderBy(m => m.MediaTypeId);
            return mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBaseViewModel>>(o);
        }

        public MediaTypeBaseViewModel MediaTypeGetById(int id)
        {
            var obj = ds.MediaTypes.Find(id);
            return obj == null ? null : mapper.Map<MediaType, MediaTypeBaseViewModel>(obj);
        }

        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll()
        {
            var o = ds.Playlists.Include("Tracks").OrderBy(m => m.Name);
            return mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBaseViewModel>>(o);
        }

        public PlaylistWithDetailViewModel PlaylistGetByIdWithDetail(int id)
        {
            var obj = ds.Playlists.Include("Tracks").SingleOrDefault(p => p.PlaylistId == id);
            return obj == null ? null : mapper.Map<Playlist, PlaylistWithDetailViewModel>(obj);
        }
        
        public PlaylistWithDetailViewModel PlaylistEditTracks(PlaylistEditTracksViewModel newItem)
        {
            var obj = ds.Playlists.Include("Tracks").SingleOrDefault(t => t.PlaylistId == newItem.PlaylistId);

            if (obj == null)
            {
                return null;
            }
            else
            {
                // update values except identifiers and navigation properties
                ds.Entry(obj).CurrentValues.SetValues(newItem);

                // update navigation properties
                obj.Tracks.Clear();
                foreach (var trackId in newItem.TrackIds)
                {
                    var t = ds.Tracks.Find(trackId);
                    obj.Tracks.Add(t);
                }

                ds.SaveChanges();

                return mapper.Map<Playlist, PlaylistWithDetailViewModel>(obj);
            }
        }
    }
}