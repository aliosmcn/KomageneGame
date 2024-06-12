
using UnityEngine;

/// Kronometre sınıfı.
/// Bu sınıftan kalıtım alan sınıflar kronometre
/// özelliğine sahip olur.
/// Sınıf fonksiyonlar aracılığı ile yönetilir.
public abstract class MonoTimer : MonoBehaviour
{
    /// Set edilen zaman
    private float settedTime = 5f;
    /// Timer'ın çalışıp çalışmadığını kontrol eden değişken
    private bool isStarted = false;
    /// Canlı olarak kalan zamanı tutan değişken.
    private float remainingTime = 99999999999;
    
    protected virtual void Update()
    {
        if (!isStarted)
            return;
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            TimeIsUp();
            StopTimer();
        }
    }
    
    /// settedTime değişkenini değiştirir.
    /// settedTime değişkeninin değişmesi ile
    /// kronometrenin ne kadar sürede çalışacağı
    /// belirlenmiş olur.
    /// <param name="remTime"></param>
    protected void SetRemainingTime(float remTime)
    {
        this.settedTime = remTime;
        this.remainingTime = remTime;
    }
    /// Kronometreyi başlatır.
    protected void StartTimer()
    {
        isStarted = true;
    }
    /// Kronometreyi durdurur.
    protected void PauseTimer()
    {
        isStarted = false;
    }
    /// Kronometrenin kalan zamanını daha önceden
    /// set edilen zamana geri getirir.
    protected void ResetTimer()
    {
        this.remainingTime = settedTime;
        isStarted = true;
    }
    /// Kronometreyi durdurur ve sıfırlar.
    protected void StopTimer()
    {
        PauseTimer();
        ResetTimer();
    }
    /// Kronometreyi sıfırlar ve yeniden başlatır.
    protected void RestartTimer()
    {
        StopTimer();
        StartTimer();
    }
    /// Kronometre de set edilen zaman
    /// geçtiğinde tetiklenir ve sürenin
    /// dolduğunu bildirir.
    protected abstract void TimeIsUp();

}
