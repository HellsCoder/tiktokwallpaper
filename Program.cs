using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace TikTokWalls
{
    class Program
    {
        public static int lastSubsCount = 0;
        private static readonly HttpClient client = new HttpClient();

        const string COOKIE_STR = "ВАШИ КУКИ ИЗ СТАТИСТИКИ";
        const string GET_CONTENT = "[{\"insigh_type\":\"vv_history\",\"days\":8},{\"insigh_type\":\"follower_num_history\",\"days\":9},{\"insigh_type\":\"pv_history\",\"days\":8},{\"insigh_type\":\"follower_num\"},{\"insigh_type\":\"user_info\"}]";

        /*
         * TikTok API Classes
         */


        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class LogPb
        {
            public string impr_id { get; set; }
        }

        public class FollowerNum
        {
            public int value { get; set; }
            public int status { get; set; }
        }

        public class FollowerNumHistory
        {
            public int value { get; set; }
            public int status { get; set; }
        }

        public class Extra
        {
            public long now { get; set; }
            public List<object> fatal_item_ids { get; set; }
            public string logid { get; set; }
        }

        public class VvHistory
        {
            public int value { get; set; }
            public int status { get; set; }
        }

        public class PvHistory
        {
            public int value { get; set; }
            public int status { get; set; }
        }

        public class Avatar168x168
        {
            public string uri { get; set; }
            public List<string> url_list { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class VideoIcon
        {
            public List<object> url_list { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string uri { get; set; }
        }

        public class AvatarLarger
        {
            public List<string> url_list { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string uri { get; set; }
        }

        public class CoverUrl
        {
            public string uri { get; set; }
            public List<string> url_list { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class AvatarThumb
        {
            public string uri { get; set; }
            public List<string> url_list { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class ShareQrcodeUrl
        {
            public List<object> url_list { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string uri { get; set; }
        }

        public class ShareInfo
        {
            public string share_title_other { get; set; }
            public string share_desc_info { get; set; }
            public string share_url { get; set; }
            public string share_weibo_desc { get; set; }
            public string share_desc { get; set; }
            public string share_title { get; set; }
            public ShareQrcodeUrl share_qrcode_url { get; set; }
            public string share_title_myself { get; set; }
        }

        public class AvatarMedium
        {
            public List<string> url_list { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string uri { get; set; }
        }

        public class Avatar300x300
        {
            public string uri { get; set; }
            public List<string> url_list { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class OriginalMusician
        {
            public int music_count { get; set; }
            public int music_used_count { get; set; }
            public int digg_count { get; set; }
        }

        public class UserInfo
        {
            public Avatar168x168 avatar_168x168 { get; set; }
            public int following_count { get; set; }
            public object platform_sync_info { get; set; }
            public int follower_status { get; set; }
            public bool has_insights { get; set; }
            public int live_agreement { get; set; }
            public bool with_shop_entry { get; set; }
            public string share_qrcode_uri { get; set; }
            public int favoriting_count { get; set; }
            public int special_lock { get; set; }
            public int shield_follow_notice { get; set; }
            public int verification_type { get; set; }
            public int status { get; set; }
            public object homepage_bottom_toast { get; set; }
            public int need_recommend { get; set; }
            public int tw_expire_time { get; set; }
            public int commerce_user_level { get; set; }
            public string ins_id { get; set; }
            public string custom_verify { get; set; }
            public string unique_id { get; set; }
            public string language { get; set; }
            public bool is_star { get; set; }
            public bool hide_search { get; set; }
            public int room_id { get; set; }
            public bool is_discipline_member { get; set; }
            public bool is_phone_binded { get; set; }
            public int react_setting { get; set; }
            public int comment_filter_status { get; set; }
            public string verify_info { get; set; }
            public string enterprise_verify_reason { get; set; }
            public int unique_id_modify_time { get; set; }
            public string twitter_id { get; set; }
            public VideoIcon video_icon { get; set; }
            public int download_setting { get; set; }
            public bool live_commerce { get; set; }
            public int user_period { get; set; }
            public string uid { get; set; }
            public string nickname { get; set; }
            public AvatarLarger avatar_larger { get; set; }
            public bool show_image_bubble { get; set; }
            public string cv_level { get; set; }
            public int stitch_setting { get; set; }
            public string avatar_uri { get; set; }
            public int user_rate { get; set; }
            public List<CoverUrl> cover_url { get; set; }
            public object ad_cover_url { get; set; }
            public bool has_facebook_token { get; set; }
            public int youtube_expire_time { get; set; }
            public bool is_ad_fake { get; set; }
            public bool user_canceled { get; set; }
            public object relative_users { get; set; }
            public object bold_fields { get; set; }
            public int follower_count { get; set; }
            public int live_verify { get; set; }
            public string short_id { get; set; }
            public string region { get; set; }
            public string account_region { get; set; }
            public int duet_setting { get; set; }
            public string google_account { get; set; }
            public string youtube_channel_id { get; set; }
            public bool accept_private_policy { get; set; }
            public AvatarThumb avatar_thumb { get; set; }
            public bool has_youtube_token { get; set; }
            public ShareInfo share_info { get; set; }
            public bool with_commerce_entry { get; set; }
            public object geofencing { get; set; }
            public object item_list { get; set; }
            public object need_points { get; set; }
            public object user_tags { get; set; }
            public string signature { get; set; }
            public int total_favorited { get; set; }
            public int shield_digg_notice { get; set; }
            public int secret { get; set; }
            public AvatarMedium avatar_medium { get; set; }
            public int shield_comment_notice { get; set; }
            public int download_prompt_ts { get; set; }
            public object can_set_geofencing { get; set; }
            public string twitter_name { get; set; }
            public bool has_email { get; set; }
            public object type_label { get; set; }
            public object cha_list { get; set; }
            public int aweme_count { get; set; }
            public bool is_block { get; set; }
            public int fb_expire_time { get; set; }
            public int apple_account { get; set; }
            public object white_cover_url { get; set; }
            public bool prevent_download { get; set; }
            public string youtube_channel_title { get; set; }
            public int user_mode { get; set; }
            public Avatar300x300 avatar_300x300 { get; set; }
            public string bind_phone { get; set; }
            public int authority_status { get; set; }
            public object followers_detail { get; set; }
            public bool has_orders { get; set; }
            public int open_insight_time { get; set; }
            public int comment_setting { get; set; }
            public object new_story_cover { get; set; }
            public string sec_uid { get; set; }
            public int follow_status { get; set; }
            public bool has_twitter_token { get; set; }
            public OriginalMusician original_musician { get; set; }
            public int create_time { get; set; }
        }

        public class Root
        {
            public LogPb log_pb { get; set; }
            public FollowerNum follower_num { get; set; }
            public List<FollowerNumHistory> follower_num_history { get; set; }
            public object follower_active_history_hours { get; set; }
            public Extra extra { get; set; }
            public List<VvHistory> vv_history { get; set; }
            public List<PvHistory> pv_history { get; set; }
            public UserInfo user_info { get; set; }
            public object follower_active_history_days { get; set; }
        }

        public sealed class Wallpaper
        {
            Wallpaper() { }

            const int SPI_SETDESKWALLPAPER = 20;
            const int SPIF_UPDATEINIFILE = 0x01;
            const int SPIF_SENDWININICHANGE = 0x02;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

            public enum Style : int
            {
                Tiled,
                Centered,
                Stretched
            }

            public static void Set(Image img, Style style)
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
                img.Save(tempPath, ImageFormat.Bmp);

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                if (style == Style.Stretched)
                {
                    key.SetValue(@"WallpaperStyle", 2.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (style == Style.Centered)
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (style == Style.Tiled)
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 1.ToString());
                }

                SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0,
                    tempPath,
                    SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
        }



        static async Task Main(string[] args)
        {
            await PostRequestAsync();
            await RunPeriodicallyAsync(PostRequestAsync, 1000 * 60 * 5);
        }

        public static async Task RunPeriodicallyAsync(
    Func<Task> func,
    int interval)
        {
            while (true)
            {
                await Task.Delay(interval);
                await func();
            }
        }

        private static async Task PostRequestAsync()
        {
            WebRequest request = WebRequest.Create("https://api.tiktok.com/aweme/v1/data/insighs/?tz_offset=7200&aid=1233&carrier_region=RU");
            request.Method = "POST";
            string data = "type_requests=" + GET_CONTENT;
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Set(HttpRequestHeader.Cookie, COOKIE_STR);
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Root json = JsonConvert.DeserializeObject<Root>(reader.ReadToEnd());
                    
                    Image img = DrawWall(json.user_info.follower_count, new Font(FontFamily.GenericSansSerif, 80, FontStyle.Bold), Color.White, Color.Black);
                    Wallpaper.Set(img, Wallpaper.Style.Centered);
                }
            }
            response.Close();
        }

        public static Image DrawWall(int count, Font font, Color textColor, Color backColor)
        {
            string text = count.ToString();
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            SizeF textSize = drawing.MeasureString(text, font);
            img.Dispose();
            drawing.Dispose();

            //img = new Bitmap((int)textSize.Width, (int)textSize.Height);
            img = new Bitmap(1920, 1080);
            drawing = Graphics.FromImage(img);
            drawing.Clear(backColor);
            Brush textBrush = new SolidBrush(textColor);

            float subX = (1920 / 2) - (textSize.Width / 2);
            float subY = (1080 / 2) - (textSize.Height / 2);
            Font sFont = new Font(FontFamily.GenericSansSerif, 30);

            drawing.DrawString(text, font, textBrush, subX, subY);
            if(count > lastSubsCount)
            {
                drawing.DrawString("+ " + (count - lastSubsCount).ToString(), sFont, new SolidBrush(Color.Green), subX, subY + textSize.Height - 10);
            }else if(count < lastSubsCount)
            {
                drawing.DrawString("- " + (lastSubsCount - count).ToString(), sFont, new SolidBrush(Color.Red), subX, subY + textSize.Height - 10);
            }


            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            lastSubsCount = count;

            return img;
        }
    }
}
