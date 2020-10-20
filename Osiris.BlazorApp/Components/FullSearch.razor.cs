using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Osiris.DogApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Osiris.BlazorApp.Components
{
    public class FullSearchBase : ComponentBase
    {
        //[Inject] Logger<FullSearchBase> _logger { get; set; }

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



        protected async override void OnInitialized()
        {
            FilteredDogs = new List<Image>();
            DebounceTimer = new Timer(500);
            DebounceTimer.Elapsed += OnUserFinish;
            DebounceTimer.AutoReset = false;
            await FilteredDogsChanged.InvokeAsync(_initialDogs);

        }

        protected string SearchString { set; get; } = "";

        private Timer DebounceTimer;
        protected void HandleKeyUp()
        {
            DebounceTimer.Stop();
            DebounceTimer.Start();
        }
        private async void OnUserFinish(Object source, ElapsedEventArgs e)
        {
            FilteredDogs = Dogs.Where(FilterName).ToList();

            if (FilteredDogs == null)
                await FilteredDogsChanged.InvokeAsync(_initialDogs);
            else
                await FilteredDogsChanged.InvokeAsync(FilteredDogs);

        }

        public bool FilterName(Image image)
        {
            Console.WriteLine($"Filtering");

            bool result = false;
            string name = string.Empty;

            if (image.breeds != null && image.breeds.Count > 0)
            {
                name = image.breeds.First().name.ToLower();
                if (name.Contains(SearchString.ToLower()))
                    result = true;

            }

            Console.WriteLine($"{name}.contains({SearchString}) = {result}");

            //if (_logger != null)
            //    _logger.LogInformation($"{name}.contains({SearchString}) = {result}");

            return result;
        }

    }
}
