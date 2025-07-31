

void mandel_float(float2 uv, float2 focus, UnityTexture2D tex, UnitySamplerState sam, out float4 RGBA)
{
    float4 ret = { 0, 0, 0, 0 };
    float r = 0.01 + abs(uv.x - focus.x) * 0.025; //distance(uv, focus) * 0.05;
    for (int cx = 0; cx < 16; cx++)
    {
        float angle = 2.f * 3.14159268 * cx / 15;
        float2 off = { sin(angle), cos(angle) };
        RGBA += SAMPLE_TEXTURE2D(tex, sam, uv + r * off);
    }
    RGBA /= 16;

}
