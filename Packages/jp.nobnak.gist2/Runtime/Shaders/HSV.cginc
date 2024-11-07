#ifndef __HSV__
#define __HSV__



static const float4 RGB2HSV_K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
static const float4 HSV2RGB_K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);



float3 HSVSaturate(float3 hsv) {
	return float3(frac(hsv.x), saturate(hsv.yz));
}
float3 RGB2HSV(float3 c) {
    float4 p = c.g < c.b ? float4(c.bg, RGB2HSV_K.wz) : float4(c.gb, RGB2HSV_K.xy);
    float4 q = c.r < p.x ? float4(p.xyw, c.r) : float4(c.r, p.yzx);

    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

float3 HSV2RGB(float3 c) {
    float3 p = abs(frac(c.xxx + HSV2RGB_K.xyz) * 6.0 - HSV2RGB_K.www);
    return c.z * lerp(HSV2RGB_K.xxx, saturate(p - HSV2RGB_K.xxx), c.y);
}



#endif