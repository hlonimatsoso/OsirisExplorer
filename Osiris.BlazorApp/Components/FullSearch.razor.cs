using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Osiris.DogApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Osiris.BlazorApp.Components
{
    public class FullSearchBase : ComponentBase
    {
        private List<Image> _initialDogs;

        private List<Image> _dogs;
        [Parameter]
        public List<Image> Dogs
        {
            get { return _dogs; }

            set
            {
                if (value != null)
                {
                    _dogs = value;
                    _initialDogs = value;

                }
            }
        }

        private List<Image> _filteredList;

        public int Count
        {
            get
            {
                return FilteredDogs == null ? 0 : FilteredDogs.Count;
            }
        }

        [Parameter] public List<Image> FilteredDogs { get; set; }


        [Parameter] public EventCallback<string> SearchStringChanged { get; set; }

        [Parameter] public EventCallback<List<Image>> FilteredDogsChanged { get; set; }

        [Parameter] public EventCallback<List<Image>> DogsChanged { get; set; }

        public string BreedList
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Dogs != null)
                    foreach (Image dog in Dogs)
                    {

                        if (dog.breeds != null && dog.breeds.Count > 0)
                            sb.Append(",").Append(dog.breeds.First().breed_group);
                    }
                var list = sb.ToString().Split(',').ToList().Distinct();

                return string.Join(',', list);
            }
        }
        public string NameList { get; set; }


        protected async override void OnInitialized()
        {
            FilteredDogs = new List<Image>();
            DebounceTimer = new Timer(500);
            DebounceTimer.Elapsed += OnUserFinish;
            DebounceTimer.AutoReset = false;
            await FilteredDogsChanged.InvokeAsync(_initialDogs);

        }

        protected string NameSearchString { set; get; } = "";

        protected string BreedSearchString { set; get; } = "";


        private Timer DebounceTimer;
        protected void HandleKeyUp()
        {
            DebounceTimer.Stop();
            DebounceTimer.Start();
        }
        private async void OnUserFinish(Object source, ElapsedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameSearchString) && string.IsNullOrEmpty(BreedSearchString))
            {
                await FilteredDogsChanged.InvokeAsync(_initialDogs);
                return;


            }

            FilteredDogs = Dogs.Where(FilterName)
                               .Where(FilterBreed).ToList();

            if (FilteredDogs == null)
                await FilteredDogsChanged.InvokeAsync(_initialDogs);
            else
                await FilteredDogsChanged.InvokeAsync(FilteredDogs);

        }

        public bool FilterName(Image image)
        {
            if (string.IsNullOrEmpty(NameSearchString))
                return true;

            Console.WriteLine($"Filtering Name");

            bool result = false;
            string name = string.Empty;

            if (image.breeds != null && image.breeds.Count > 0)
            {
                name = image.breeds.First().name.ToLower();
                if (name.Contains(NameSearchString.ToLower()))
                    result = true;

            }

            Console.WriteLine($"{name}.contains({NameSearchString}) = {result}");

            return result;
        }

        public bool FilterBreed(Image image)
        {
            if (string.IsNullOrEmpty(BreedSearchString))
                return true;

            Console.WriteLine($"Filtering Breed");

            bool result = false;
            string name = string.Empty;

            if (image.breeds != null && image.breeds.Count > 0)
            {
                if (!string.IsNullOrEmpty(image.breeds.First().breed_group))
                {
                    name = image.breeds.First().breed_group.ToLower();
                    if (name.Contains(BreedSearchString.ToLower()))
                        result = true;
                }

            }

            Console.WriteLine($"{name}.contains({NameSearchString}) = {result}");

            return result;
        }

    }
}
