using Newtonsoft.Json;

namespace Application.Common.ExternalApi.UdemyApi
{
    public class CourseListResponse
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("next")]
        public Uri Next { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("aggregations")]
        public List<Aggregation> Aggregations { get; set; }

        [JsonProperty("search_tracking_id")]
        public string SearchTrackingId { get; set; }

        [JsonProperty("boosted_language")]
        public string BoostedLanguage { get; set; }
    }

    public class Aggregation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }
    }

    public class Option
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Result
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("is_paid")]
        public bool IsPaid { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("price_detail")]
        public PriceDetail PriceDetail { get; set; }

        [JsonProperty("price_serve_tracking_id")]
        public string PriceServeTrackingId { get; set; }

        [JsonProperty("visible_instructors")]
        public List<VisibleInstructor> VisibleInstructors { get; set; }

        [JsonProperty("image_125_H")]
        public Uri Image125_H { get; set; }

        [JsonProperty("image_240x135")]
        public Uri Image240X135 { get; set; }

        [JsonProperty("is_practice_test_course")]
        public bool IsPracticeTestCourse { get; set; }

        [JsonProperty("image_480x270")]
        public Uri Image480X270 { get; set; }

        [JsonProperty("published_title")]
        public string PublishedTitle { get; set; }

        [JsonProperty("tracking_id")]
        public string TrackingId { get; set; }

        [JsonProperty("predictive_score")]
        public object PredictiveScore { get; set; }

        [JsonProperty("relevancy_score")]
        public object RelevancyScore { get; set; }

        [JsonProperty("input_features")]
        public object InputFeatures { get; set; }

        [JsonProperty("lecture_search_result")]
        public object LectureSearchResult { get; set; }

        [JsonProperty("curriculum_lectures")]
        public List<object> CurriculumLectures { get; set; }

        [JsonProperty("order_in_results")]
        public object OrderInResults { get; set; }

        [JsonProperty("curriculum_items")]
        public List<object> CurriculumItems { get; set; }

        [JsonProperty("headline")]
        public string Headline { get; set; }

        [JsonProperty("instructor_name")]
        public object InstructorName { get; set; }
    }

    public class PriceDetail
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("price_string")]
        public string PriceString { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class VisibleInstructor
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("job_title")]
        public string JobTitle { get; set; }

        [JsonProperty("image_50x50")]
        public Uri Image50X50 { get; set; }

        [JsonProperty("image_100x100")]
        public Uri Image100X100 { get; set; }

        [JsonProperty("initials")]
        public string Initials { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}